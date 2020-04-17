using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspose.Cloud.Marketplace.HTML.Converter.Services
{
    /// <summary>
    /// Concept version for Statistical service.
    /// </summary>
    public class StatisticalService
    {
            private readonly ConcurrentDictionary<string, int> _stats
                = new ConcurrentDictionary<string, int>();

            /// <summary>
            /// Stats dictionary is used for counting converted docs per machine.
            /// </summary>
            public ConcurrentDictionary<string, int> Stats => _stats;

        /// <summary>
        /// The method counts converted documents
        /// </summary>
        /// <param name="converterOptionsMachineId">VS Code MachineID value</param>
        public void IncrementCounter(string converterOptionsMachineId)
            {
                if (string.IsNullOrEmpty(converterOptionsMachineId))
                    converterOptionsMachineId = "unknown";
                _stats.AddOrUpdate(converterOptionsMachineId, 1, (key, oldValue) => oldValue + 1);
            }
    }
}
