using BusinessObject;

namespace DataAccess
{
    public class VoicePostDAO
    {
        private static VoicePostDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoicePostDAO() { }
        public static VoicePostDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoicePostDAO();
                    }
                    return instance;
                }
            }
        }

        public VoicePost AddVoice(VoicePost voicePost)
        {
            VoicePost result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoicePosts.Add(voicePost).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

    }
}
