using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class TreningController : Controller
    {
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private TrainingRoomService _trainingRoomService = new TrainingRoomService();
        private UserService _userService = new UserService();
        // GET: Trening
        public ActionResult TrainingRoom()
        {
            TrainingRoomViewModel trainingRoomVM = _complementVMService.ComplementTrainingRoomVM();

            return View(trainingRoomVM);
        }

        public JsonResult GetExerciseCategories(string distance)
        {
            var json = _trainingRoomService.GetExerciseCategories(distance);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExerciseData(string exerciseName)
        {
            string json = _trainingRoomService.GetExerciseData(exerciseName);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartTraining(string btnId, string exerciseName)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            bool isBusy = _userService.GetCurrentUserCharacter().IsBusy;
            if (isBusy)
            {
                resultDictionary.Add("Status", "Busy");
            }
            else
            {
                bool isSuccess = _trainingRoomService.StartTraining(btnId, exerciseName);
                if (isSuccess)
                {
                    resultDictionary.Add("Status", "Ok");
                }
                else
                {
                    resultDictionary.Add("Status", "Missing");
                }
            }

            string json = JsonConvert.SerializeObject(resultDictionary);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public void CancelTraining(string btnId, string exerciseName)
        {
            _trainingRoomService.CancelTraining(btnId, exerciseName);
        }
    }


}