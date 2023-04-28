using CIPlatform.Models;
using CIPlatform.Models.ViewDataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.DataAccess.Repository.IRepository
{
    public interface IMissionRepository
    {
        public IQueryable<MissionListingCard> getMissions();
        public List<CommentUserInfo> CommentByMissionUserId(long missionId);
        public void AddComment(string comment, long missionId,long userId);

        public bool ApplyMission(long missionId, long userId);
        public void UpdateRate(MissionRating missionRating);
        public void addRate(MissionRating missionRating);
    }
}
