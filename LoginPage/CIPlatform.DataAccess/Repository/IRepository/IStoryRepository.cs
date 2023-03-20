﻿using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IStoryRepository
    {
        public List<StoryListingViewModel> getAllStories();
    }
}