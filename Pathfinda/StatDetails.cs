using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda
{
    public class StatDetails : INotifyPropertyChanged
    {
        private SortedList<string, double> _contributingFactors;
        public SortedList<string, double> ContributingFactors
        {
            get
            {
                return _contributingFactors;
            }
        }

        public StatDetails AddContributingFactor(string sourceOfContribution, double amountContributed)
        {
            _contributingFactors.Add(sourceOfContribution, amountContributed);
            Notify("ContributingFactors");
            Notify("Description");
            Notify("Total");
            return this;
        }

        /// <summary>
        /// Human readable description of what factors went into calculating the given stat
        /// </summary>
        public string Description
        {
            get
            {
                return string.Join(Environment.NewLine, ContributingFactors.Select(x => x.Value.ToString("+0;-#") + " from " + x.Key));
            }
        }

        public double Total
        {
            get
            {
                return ContributingFactors.Sum(x => x.Value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatDetails()
        {
            _contributingFactors = new SortedList<string, double>();
        }

        public static StatDetails operator +(StatDetails one, StatDetails two)
        {            
            if (one == null)
                if (two == null)
                    return null;
                else
                    return two;
            else if (two == null)
                return one;
            var returnMe = new StatDetails();
            foreach (var factor in one.ContributingFactors.Concat(two.ContributingFactors))
                returnMe.ContributingFactors.Add(factor.Key, factor.Value);
            return returnMe;
        }
    }
}
