using TelegramBot.Models;
using System.Collections.Concurrent;

namespace TelegramBot.Services
{
    class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long charId)
        {
            if (_sessions.ContainsKey(charId))
                return _sessions[charId];

            var newSession = new Session { Function = "str_length" };
            _sessions.TryAdd(charId, newSession);
            return newSession;
        }
    }
}
