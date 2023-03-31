namespace Keepr.Services
{
    public class ProfliesService
    {
        private readonly ProfilesRepository _repo;
        public ProfliesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfileById(string id)
        {
            Profile profile = _repo.GetProfileById(id);
            if (profile == null) throw new Exception($"No Profile at id: {id}");
            return profile;
        }
    }
}