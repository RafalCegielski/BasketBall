using BasketBallMVC.DAL;
using BasketBallMVC.Model;
using BasketBallMVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BasketBallMVC.Services
{
    public class ShopService
    {
        private TrainingRoomService _trainingRoomService = new TrainingRoomService();

        public string GetCategoriesForShop(string buttonId)
        {
            using (var db = new BasketBallContext())
            {
                var nameOfButtonList = Regex.Split(buttonId, "_").ToList();
                ShopCategory shopCategory = new ShopCategory();
                DeviceCategory deviceCategory = new DeviceCategory();
                string json = string.Empty;

                if (nameOfButtonList[0] == "CategoryButton")
                {
                    string name = nameOfButtonList[1];
                    shopCategory = db.ShopCategories.FirstOrDefault(x => x.Name == name);
                    List<CategorieNamesViewModel> categorieNameList = new List<CategorieNamesViewModel>();

                    foreach (var item in shopCategory.DeviceCategories)
                    {
                        categorieNameList.Add(new CategorieNamesViewModel { name = item.Name, deviceMarker = item.deviceMarker });
                    }
                    categorieNameList.Sort((s1, s2) => s2.deviceMarker.CompareTo(s1.deviceMarker));

                    json = JsonConvert.SerializeObject(categorieNameList);
                }
                else if (nameOfButtonList[0] == "DeviceCategoryButton")
                {
                    var listOFOurDevices = _trainingRoomService.GetAllOurDevices();

                    string name = nameOfButtonList[1];
                    deviceCategory = db.DeviceCategories.FirstOrDefault(x => x.Name == name);
                    List<DeviceDetailsForShopViewModel> deviceCategorieNameList = new List<DeviceDetailsForShopViewModel>();

                    foreach (var item in deviceCategory.Devices)
                    {
                        if (listOFOurDevices.Any(x => x.DeviceID == item.DeviceID))
                        {
                            deviceCategorieNameList.Add(new DeviceDetailsForShopViewModel { Name = item.Name, Description = item.Description, Price = item.Price.ToString(), isOur = true, Id = item.DeviceID.ToString() });
                        }
                        else
                        {
                            deviceCategorieNameList.Add(new DeviceDetailsForShopViewModel { Name = item.Name, Description = item.Description, Price = item.Price.ToString(), isOur = false, Id = item.DeviceID.ToString() });
                        }

                    }
                    deviceCategorieNameList.Sort((s1, s2) => s2.Price.CompareTo(s1.Price));

                    json = JsonConvert.SerializeObject(deviceCategorieNameList);
                }

                return json;
            }
        }

        public string BuyNewDevice(string buttonId)
        {
            using (var db = new BasketBallContext())
            {
                var deviceId = buttonId.Replace("DeviceButton_", "");
                var device = db.Devices.FirstOrDefault(x => x.DeviceID.ToString() == deviceId);
                var user = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();

                if (character.Gold >= device.Price)
                {
                    var allOwnDevices = db.TraningRoomByDevices.Where(x => x.Device.DeviceCategory.DeviceCategoryId == device.DeviceCategory.DeviceCategoryId).ToList();

                    int maxValue = 0;
                    if (allOwnDevices != null && allOwnDevices.Count != 0)
                    {
                        foreach (var item in allOwnDevices)
                        {
                            var value = Regex.Match(item.Device.Description, @"\d+").ToString();
                            if (maxValue < int.Parse(value))
                            {
                                maxValue = int.Parse(value);
                            }
                        }
                    }
                    var valueTargetDevice = Regex.Match(device.Description, @"\d+").ToString();
                    if (maxValue < int.Parse(valueTargetDevice))
                    {
                        var valueDifference = int.Parse(valueTargetDevice) - maxValue;
                        switch (device.DeviceCategory.ShopCategory.Name)
                        {
                            case "Siła":
                                character.Strengh += valueDifference;
                                break;
                            case "Szybkość":
                                character.Speed += valueDifference;
                                break;
                            case "Celność":
                                character.Marksmanship += valueDifference;
                                break;
                            case "Obrona":
                                character.Defence += valueDifference;
                                break;
                        }
                    }


                    var traningRoom = db.TrainingRooms.FirstOrDefault(x => x.Character.CharacterID == character.CharacterID);
                    character.Gold -= device.Price;
                    db.TraningRoomByDevices.Add(new TraningRoomByDevice { BuyDate = DateTime.Now, Device = device, TraningRoomByDeviceID = Guid.NewGuid(), TraningRoom = traningRoom });
                    db.SaveChanges();


                    dictionary.Add("status", "true");
                    var json = JsonConvert.SerializeObject(dictionary);

                    return json;
                }
                else
                {
                    dictionary.Add("status", "false");
                    var json = JsonConvert.SerializeObject(dictionary);

                    return json;
                }
            }
        }

        public void SaleDevice(string buttonId)
        {
            using (var db = new BasketBallContext())
            {
                var deviceId = buttonId.Replace("DeviceSaleButton_", "");
                var device = db.Devices.FirstOrDefault(x => x.DeviceID.ToString() == deviceId);
                var user = db.Users.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name);
                var character = db.Characters.FirstOrDefault(x => x.UserId == user.Id);

                var allOwnDevices = db.TraningRoomByDevices.Where(x => x.Device.DeviceCategory.DeviceCategoryId == device.DeviceCategory.DeviceCategoryId).ToList();
                int maxValue = 0;
                int almostMaxValue = 0;
                if (allOwnDevices != null && allOwnDevices.Count != 0)
                {
                    foreach (var item in allOwnDevices)
                    {
                        var value = Regex.Match(item.Device.Description, @"\d+").ToString();
                        if (maxValue < int.Parse(value))
                        {
                            maxValue = int.Parse(value);
                        }
                    }
                    foreach (var item in allOwnDevices)
                    {
                        var value = Regex.Match(item.Device.Description, @"\d+").ToString();
                        if (maxValue > int.Parse(value) && almostMaxValue < int.Parse(value))
                        {
                            almostMaxValue = int.Parse(value);
                        }
                    }
                }
                var valueTargetDevice = Regex.Match(device.Description, @"\d+").ToString();
                if (maxValue == int.Parse(valueTargetDevice) && almostMaxValue != 0)
                {
                    var valueDifference = maxValue - almostMaxValue;
                    switch (device.DeviceCategory.ShopCategory.Name)
                    {
                        case "Siła":
                            character.Strengh -= valueDifference;
                            break;
                        case "Szybkość":
                            character.Speed -= valueDifference;
                            break;
                        case "Celność":
                            character.Marksmanship -= valueDifference;
                            break;
                        case "Obrona":
                            character.Defence -= valueDifference;
                            break;
                    }
                }
                else if (maxValue == int.Parse(valueTargetDevice))
                {
                    switch (device.DeviceCategory.ShopCategory.Name)
                    {
                        case "Siła":
                            character.Strengh -= maxValue;
                            break;
                        case "Szybkość":
                            character.Speed -= maxValue;
                            break;
                        case "Celność":
                            character.Marksmanship -= maxValue;
                            break;
                        case "Obrona":
                            character.Defence -= maxValue;
                            break;
                    }
                }

                character.Gold += device.Price / 2;
                var traningRoomByDevices = db.TraningRoomByDevices.FirstOrDefault(x => x.Device.DeviceID == device.DeviceID);
                db.TraningRoomByDevices.Remove(traningRoomByDevices);
                db.SaveChanges();
            }
        }
    }
}
