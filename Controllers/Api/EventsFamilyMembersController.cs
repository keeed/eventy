using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eventy.Data;
using eventy.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eventy.Controllers.Api
{
    [Route("api/[controller]")]
    public class EventsFamilyMembersController : Controller
    {
        public EventyDbContext Context { get; }

        public EventsFamilyMembersController(EventyDbContext context)
        {
            Context = context;
        }

        [HttpPost]
        public async Task<JsonResult> AddAttendance(AddAttendanceViewModel addAttendanceViewModel)
        {
            bool isSuccessful = false;

            var eventId = addAttendanceViewModel.EventId;
            var familyId = addAttendanceViewModel.FamilyMemberId;

            var isExisting =  Context.EventsFamilyMembers.Any(efm =>
                efm.FamilyMemberId == addAttendanceViewModel.FamilyMemberId &&
                efm.EventId == addAttendanceViewModel.FamilyMemberId);

            if (!isExisting)
            {
                Context.EventsFamilyMembers.Add(
                    new EventsFamilyMembers() {
                        EventId = addAttendanceViewModel.EventId,
                        FamilyMemberId = addAttendanceViewModel.FamilyMemberId
                    }
                );

                isSuccessful = true;
            }

            return Json(new
            {
                success = isSuccessful
            });
        }
        // // GET: api/<controller>
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // // GET api/<controller>/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/<controller>
        // [HttpPost]
        // public void Post([FromBody]string value)
        // {
        // }

        // // PUT api/<controller>/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/<controller>/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
