using BidOneCoding.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BidOneCoding.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<ValidationController> _logger;

        public ValidationController(IPersonRepository personRepository, ILogger<ValidationController> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ValidateFirstName(string firstName)
        {
            _logger.LogInformation($"ValidationController:ValidateFirstName => Validating First name: {firstName}");
            if (_personRepository.GetPerson(firstName) != null)
            {
                _logger.LogInformation($"ValidationController:ValidateFirstName => First name already exists: {firstName}");
                return Json(data: "First name already exists");
            }

            return Json(data: true);
        }
    }
}
