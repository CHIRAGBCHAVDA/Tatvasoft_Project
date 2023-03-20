﻿using CIPlatform.Data;
using CIPlatform.DataAccess.Repository.IRepository;
using CIPlatform.Models.ViewDataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository
{
    public class StoryRepository : IStoryRepository
    {
        private readonly CiplatformContext _db;
        private HttpContext _httpContext;

        public StoryRepository(CiplatformContext db, HttpContext httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }
        public List<StoryListingViewModel> getAllStories()
        {
            var stories = from s in _db.Stories join m in _db.Missions on s.MissionId equals m.MissionId
                          join u in _db.Users on s.UserId equals u.UserId
                          join mt in _db.MissionThemes on m.MissionThemeId equals mt.MissionThemeId
                          
                          select new StoryListingViewModel()
                          {
                             Stories = s,
                             Users = u,
                             Missions = m,
                             Theme = mt.Title,
                             Skills = (List<string>)(from ms in _db.MissionSkills
                                                      join s in _db.Skills on ms.SkillId equals s.SkillId
                                                      where ms.MissionId == m.MissionId
                                                      select s.SkillName),
                          };

            return stories.ToList();
        }
    }
}