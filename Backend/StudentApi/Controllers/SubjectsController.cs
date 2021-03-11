using AppDbContext.Enums;
using StudentApi.Models.ApiModels;
using StudentApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AppDbContext.Entities;

namespace StudentApi.Controllers
{
    [Route("Api/Subjects")]
    public class SubjectsController : BaseController
    {
        [Route("Api/Subjects/List")]
        [Authorize]
        [HttpGet]
        public ListResult<SubjectApiModel> List(string keyword = null)
        {
            try
            {
                if (!CheckUserPermission())
                    return new ListResult<SubjectApiModel>()
                    {
                        IsOk = true,
                        Message = new Message("Error ,Please Login as Student", MessageType.Error),
                        Items = null
                    };
                var subjs = Db.Subjects
                    .Include(cc => cc.TeacherSubjs)
                    .ToList();
                if (!string.IsNullOrEmpty(keyword))
                   subjs = subjs.Where(c => c.Name.ToLower().Contains(keyword.ToLower())).ToList();
                List<SubjectApiModel> subjects = new List<SubjectApiModel>();
                foreach (var item in subjs)
                {
                    List<TeacherApiModel> subjectTeachers = new List<TeacherApiModel>();
                    foreach(var teacher in item.TeacherSubjs)
                    {
                        Teacher te = Db.Teachers.Find(teacher.Teacher_UserId);
                        subjectTeachers.Add(new TeacherApiModel { 
                        Id = te.Id,
                        FirsName = te.FirstName,
                        LastName = te.LastName,
                        Role = Discriminator.Teacher.ToString(),
                        UserId = te.UserId
                        });
                    }
                    SubjectApiModel suApiModel = new SubjectApiModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Teachers = subjectTeachers
                    };
                    subjects.Add(suApiModel);
                }
                return new ListResult<SubjectApiModel>()
                {
                    IsOk = true,
                    Message = new Message("Success, Subjects Found Successfully", MessageType.Success),
                    Items = subjects
                };
            }
            catch (Exception ex)
            {
                return new ListResult<SubjectApiModel>()
                {
                    IsOk = false,
                    Message = new Message(ex.Message + " " + ex.InnerException?.Message, MessageType.Error),
                    Items = null
                };
            }
        }
    }
}