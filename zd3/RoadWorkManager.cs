using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd3
{
    public class RoadWorkManager
    {
        private List<RoadWork> works = new List<RoadWork>();
        private Dictionary<string, RoadWork> worksDict = new Dictionary<string, RoadWork>();

        public void AddWork(RoadWork work)
        {
            works.Add(work);
            worksDict[work.WorkName] = work;
        }

        public bool RemoveWork(RoadWork work)
        {
            bool removed = works.Remove(work);
            if (removed)
            {
                worksDict.Remove(work.WorkName);
            }
            return removed;
        }
        public (int totalWorks, int enhancedWorks, double avgQuality, double maxQuality, double minQuality, double totalQuality) GetStatistics()
        {
            int total = works.Count;
            int enhanced = works.OfType<EnhancedRoadWork>().Count();
            double avg = 0, max = 0, min = 0, totalQ = 0;

            if (works.Any())
            {
                var qualities = works.Select(w => w.CalculateQuality()).ToList();
                avg = qualities.Average();
                max = qualities.Max();
                min = qualities.Min();
                totalQ = qualities.Sum();
            }

            return (total, enhanced, avg, max, min, totalQ);
        }
        public List<RoadWork> SearchWorks(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return new List<RoadWork>(works);
            }

            return works
                .Where(w => w.WorkName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<RoadWork> GetAllWorks()
        {
            return new List<RoadWork>(works);
        }
    }
}
