using BusinessObject;
using DataAccess;
using Repository.Interface;

namespace Repository
{
    public class VoicePostRepository : IVoicePostRepository
    {
        public VoicePost AddVoice(VoicePost voicePost) => VoicePostDAO.Instance.AddVoice(voicePost);
    }
}
