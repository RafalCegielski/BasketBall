using PagedList;
using System.Collections.Generic;

namespace BasketBallMVC.ViewModel
{
    public class AdminUsersViewModel
    {
        public List<AdminUserDetails> pagedList { get; set; }
        public IPagedList<AdminUserDetails> pagedPagedList { get; set; }
    }

    public class AdminUserDetails
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public bool isBanned { get; set; }
    }
}