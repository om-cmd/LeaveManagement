using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;

namespace LeaveManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]


    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveType;
        public LeaveTypeController(ILeaveTypeService leaveType)
        {
           _leaveType = leaveType;
        }
        [HttpGet]
        public async Task<IActionResult> LeaveTypeList()
        {
            var typeList = _leaveType.LeaveTypeList();
            return Ok(typeList);
        }

        [HttpPost]
        public IActionResult CreateLeaveType(LeaveTypeViewModel model)
        {
            var typeCreate = _leaveType.CreateLeaveType(model); 
            return Ok(typeCreate);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypeViewModel model, int id)
        {
           var updateType = _leaveType.UpdateLeaveType(model, id);
            return Ok(updateType);  
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveType(int id)
        {
            var deleteType = _leaveType.DeleteLeaveType(id);    
            return Ok(deleteType);
        }

    }
}
