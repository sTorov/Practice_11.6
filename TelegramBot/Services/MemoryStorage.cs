using TelegramBot.Models;
using System.Collections.Concurrent;

namespace TelegramBot.Services
{
    /// <summary>
    /// Программное хранилище сессий
    /// </summary>
    class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long charId)
        {
            if (_sessions.ContainsKey(charId))
                return _sessions[charId];

            var newSession = new Session { Function = string.Empty };
            _sessions.TryAdd(charId, newSession);
            return newSession;
        }
    }
}
