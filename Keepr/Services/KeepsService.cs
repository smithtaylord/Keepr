namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        private readonly ProfliesService _profilesService;

        public KeepsService(KeepsRepository repo, ProfliesService profilesService)
        {
            _repo = repo;
            _profilesService = profilesService;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repo.CreateKeep(keepData);
            return keep;
        }


        internal List<Keep> GetAllKeeps()
        {
            List<Keep> keeps = _repo.GetAllKeeps();
            return keeps;
        }

        internal Keep GetOneKeep(int id, string userId)
        {
            Keep keep = _repo.GetOneKeep(id);
            if (keep == null) throw new Exception($"No keep found with id: {id}");
            {
                if (keep.CreatorId != userId)
                    keep.Views++;
                _repo.EditKeep(keep);
            }
            return keep;
        }
        internal List<Keep> GetUsersKeeps(string userId)
        {
            _profilesService.GetProfileById(userId);
            List<Keep> keeps = _repo.GetUsersKeeps(userId);
            return keeps;
        }
        internal Keep EditKeep(int keepId, Keep keepData, string userId)
        {
            Keep original = _repo.GetOneKeep(keepId);
            if (original.CreatorId != userId) throw new Exception("This is not your keep, you cannot edit");
            original.Name = keepData.Name != null ? keepData.Name : original.Name;
            original.Description = keepData.Description != null ? keepData.Description : original.Description;
            int rowsAffected = _repo.EditKeep(original);
            if (rowsAffected == 0) throw new Exception("Could not modify for some reason");
            if (rowsAffected > 1) throw new Exception("Something went very wrong and you edited more than one row");
            return original;
        }
        internal string DeleteKeep(int id, string userId)
        {
            Keep keep = _repo.GetOneKeep(id);
            if (keep == null) throw new Exception($"No keep found with id: {id}");
            if (keep.CreatorId != userId) throw new Exception("You are not the owner of this keep! Delete is not allowed!");
            _repo.DeleteKeep(id);
            return $"{keep.Name} has been deleted";
        }

    }
}