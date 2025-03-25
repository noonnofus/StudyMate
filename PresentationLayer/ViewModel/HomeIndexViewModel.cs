using DataAccessLayer.Models;

namespace PresentationLayer.ViewModel
{
    public class HomeIndexViewModel
    {
        public ApplicationUser? Users { get; set; }
        public string PageTitle { get; set; }
    }
}
