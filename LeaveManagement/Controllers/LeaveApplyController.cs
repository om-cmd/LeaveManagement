using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class LeaveApplyController : ControllerBase
    {
        private readonly ILeaveApplyService _leaveApply;

        public LeaveApplyController(ILeaveApplyService leaveApply)
        {
            _leaveApply = leaveApply;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveApplyList()
        {
            var leave = _leaveApply.GetLeaveApplyList();
            return Ok(leave);
           
        }

       
        [HttpPost]
        public IActionResult CreateLeaveApplication([FromBody] LeaveApplyViewModel model)
        {
            var createLeave = _leaveApply.CreateLeaveApplication(model);
            return Ok(createLeave);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveApplication(int id, [FromBody] LeaveApplyViewModel model)
        {
            var updateLeave = _leaveApply.UpdateLeaveApplication(id, model);
            return Ok(updateLeave);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveApplication(int id)
        {
           var delete = _leaveApply.DeleteLeaveApplication(id);
            return Ok(delete);
        }
    }
}
