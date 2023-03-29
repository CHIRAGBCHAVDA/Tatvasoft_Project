using CIPlatform.Models;
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
        public List<StoryListingViewModel> getStoriesByCountryId(string[] cid);
        public List<StoryListingViewModel> getStoriesPerPage(int pageNum, int pageSize);
        public bool deleteStory(long storyId);

        public List<Mission> getAppliedMissions(long userId);
        public ShareStoryViewModel? getDraftedStory(long userId);
        public long draftStorybyUser(string storyMissionName, string storyTitle, DateTime storyDate, string story, string? storyVideoUrl, string[]? srcs);
        public bool storeNewStory(long userId, long missionId);

        public Story getStoryBySID(long sid);
        public List<StoryMedium> storyMedia(long storyId);

    }
}
