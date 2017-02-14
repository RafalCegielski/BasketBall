using BasketBallMVC.Hubs;
using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private UserService _userService = new UserService();
        private BattleService _battleService = new BattleService();
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        private TrainingRoomService _trainingRoomService = new TrainingRoomService();
        private CharacterService _characterService = new CharacterService();
        private NotificationService _notificationService = new NotificationService();
        // GET: Game
        public ActionResult Battle()
        {
            BattleViewModel battleVM = _complementVMService.ComplementBattleVM();
            _battleService.CheckChosenAttacks(ref battleVM);


            return View(battleVM);
        }
        public ActionResult VsComputer()
        {
            var tuple = _characterService.CheckLifeAndEnergy();
            BattleViewModel battleVM = _complementVMService.ComplementBattleVM();
            _battleService.CheckChosenAttacks(ref battleVM);
            if (tuple.Item1 && tuple.Item2 && battleVM.isAttacksChosen)
            {
                var listOfAttacks = _battleService.GenerateBattleVsComputer();
                var battleLog = _battleService.GenerateBattleText(listOfAttacks);
                _characterService.ChangeStats(battleLog, true, string.Empty);

                _complementVMService.UpdateBattleVM(ref battleVM);
                battleVM.PlayerResult = int.Parse(battleLog[battleLog.Count - 1].leftSum);
                battleVM.OponentResult = int.Parse(battleLog[battleLog.Count - 1].rightSum);
                battleVM.OponentNick = "Komputer";
                battleVM.PlayerNick = _userService.GetUserNickByIdentityName();

                battleVM.battleLog = battleLog;
            }
            if (tuple.Item1)
                battleVM.isEnoughLife = true;
            else
                battleVM.isEnoughLife = false;

            if (tuple.Item2)
                battleVM.isEnoughEnergy = true;
            else
                battleVM.isEnoughEnergy = false;


            return View("Battle", battleVM);
        }
        public ActionResult VsRandomPlayer()
        {
            var tuple = _characterService.CheckLifeAndEnergy();
            BattleViewModel battleVM = _complementVMService.ComplementBattleVM();
            _battleService.CheckChosenAttacks(ref battleVM);

            if (tuple.Item1 && tuple.Item2 && battleVM.isAttacksChosen)
            {
                var oponentId = _battleService.SearchOponent();

                var listOfAttacks = _battleService.BattleVsPlayer(oponentId);
                var battleLog = _battleService.GenerateBattleText(listOfAttacks);
                _characterService.ChangeStats(battleLog, false, oponentId);

                _complementVMService.UpdateBattleVM(ref battleVM);
                battleVM.PlayerResult = int.Parse(battleLog[battleLog.Count - 1].leftSum);
                battleVM.OponentResult = int.Parse(battleLog[battleLog.Count - 1].rightSum);
                battleVM.OponentNick = _userService.GetUserNickById(oponentId);
                battleVM.PlayerNick = _userService.GetUserNickByIdentityName();

                battleVM.battleLog = battleLog;

                var invitedEmail = _userService.GetUserEmailById(oponentId);
                _notificationService.AddBattleNotification(invitedEmail);
                NotificationHub.AddNotification(invitedEmail);
            }
            if (tuple.Item1)
                battleVM.isEnoughLife = true;
            else
                battleVM.isEnoughLife = false;

            if (tuple.Item2)
                battleVM.isEnoughEnergy = true;
            else
                battleVM.isEnoughEnergy = false;


            return View("Battle", battleVM);
        }

        public ActionResult VsSelectedPlayer(string oponentNick)
        {
            var tuple = _characterService.CheckLifeAndEnergy();
            BattleViewModel battleVM = _complementVMService.ComplementBattleVM();
            _battleService.CheckChosenAttacks(ref battleVM);

            if (tuple.Item1 && tuple.Item2 && battleVM.isAttacksChosen)
            {
                var id = _userService.GetUserIdByNick(oponentNick);

                var listOfAttacks = _battleService.BattleVsPlayer(id);
                var battleLog = _battleService.GenerateBattleText(listOfAttacks);
                _characterService.ChangeStats(battleLog, false, id);

                _complementVMService.UpdateBattleVM(ref battleVM);
                battleVM.PlayerResult = int.Parse(battleLog[battleLog.Count - 1].leftSum);
                battleVM.OponentResult = int.Parse(battleLog[battleLog.Count - 1].rightSum);
                battleVM.OponentNick = _userService.GetUserNickById(id);
                battleVM.PlayerNick = _userService.GetUserNickByIdentityName();

                battleVM.battleLog = battleLog;

                var invitedEmail = _userService.GetUserEmailById(id);
                _notificationService.AddBattleNotification(invitedEmail);
                NotificationHub.AddNotification(invitedEmail);
            }
            if (tuple.Item1)
                battleVM.isEnoughLife = true;
            else
                battleVM.isEnoughLife = false;

            if (tuple.Item2)
                battleVM.isEnoughEnergy = true;
            else
                battleVM.isEnoughEnergy = false;


            return View("Battle", battleVM);
        }

        public ActionResult GameSettings()
        {
            GameSettingsViewModel gameSettingsVM = _complementVMService.ComplementGameSettingsVM();
            ViewBag.Exercises = _trainingRoomService.CreateExerciseList(ref gameSettingsVM);


            return View(gameSettingsVM);
        }
        [HttpPost]
        public ActionResult GameSettings(GameSettingsViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Exercises = _trainingRoomService.CreateExerciseList(ref vm);
                return View(vm);
            }

            _battleService.AddChoseAttacksToCharacter(vm);

            BattleViewModel battleVM = _complementVMService.ComplementBattleVM();
            _battleService.CheckChosenAttacks(ref battleVM);

            return RedirectToAction("Home", "Home");
        }

        public ActionResult GameStyle()
        {
            GameStyleViewModel GameStyleVM = _complementVMService.ComplementGameStyleVM();

            return View(GameStyleVM);
        }
        [HttpPost]
        public void GameStyle(string value)
        {
            _battleService.ChangeStyleValue(value);
        }
    }
}