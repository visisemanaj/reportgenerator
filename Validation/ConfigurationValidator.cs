using System;
using System.Collections.Generic;
using System.Linq;
using ReportGenerator.DTOs;

namespace ReportGenerator.Validation
{
    public class ConfigurationValidator
    {
        private List<string> _timeTrackerUsers;
        private List<string> _timeTrackerClients;

        public ConfigurationValidator(List<string> timeTrackerClients, List<string> timeTrackerUsers)
        {
            _timeTrackerClients = timeTrackerClients;
            _timeTrackerUsers = timeTrackerUsers;
        }

        private class ConfigurationPath
        {
            public Client Client { get; set; }

            public JobDefinition JobDefinition { get; set; }

            public User User { get; set; }

            public JobFunction JobFunction { get; set; }

            public override string ToString()
            {
                var parts = new List<string>();

                if (Client != null)
                {
                    parts.Add(Client.Name);
                }

                if (JobDefinition != null)
                {
                    parts.Add(JobDefinition.Name);
                }

                if (User != null)
                {
                    parts.Add(User.Name);
                }

                if (JobFunction != null)
                {
                    parts.Add(JobFunction.Name);
                }

                return string.Join(" => ", parts);
            }
        }

        public bool Validate(Budget budget, out List<string> errors)
        {
            var resErrors = new List<string>();
            var configPath = new ConfigurationPath();

            var registerErrors = new Action<List<string>>(localErrors =>
            {
                var prefix = string.Join(" -> ", configPath);
                if (!string.IsNullOrWhiteSpace(prefix))
                {
                    prefix += ": ";
                }

                resErrors.AddRange(localErrors.Select(x => prefix + x).ToList());
            });

            registerErrors(GetErrors(budget, configPath));

            if (budget.Clients != null)
            {
                foreach (var client in budget.Clients)
                {
                    configPath.Client = client;

                    registerErrors(GetErrors(client, configPath));

                    if (client.JobDefinition != null)
                    {
                        foreach (var jobDefinition in client.JobDefinition)
                        {
                            configPath.JobDefinition = jobDefinition;

                            registerErrors(GetErrors(jobDefinition, configPath));

                            configPath.JobDefinition = null;
                        }
                    }

                    if (client.Users != null)
                    {
                        foreach (var user in client.Users)
                        {
                            configPath.User = user;

                            registerErrors(GetErrors(user, configPath));

                            if (user.JobFunctions != null)
                            {
                                foreach (var jobFunction in user.JobFunctions)
                                {
                                    configPath.JobFunction = jobFunction;

                                    registerErrors(GetErrors(jobFunction, configPath));

                                    configPath.JobFunction = null;
                                }
                            }

                            configPath.User = null;
                        }
                    }

                    configPath.Client = null;
                }
            }

            errors = resErrors;
            return !resErrors.Any();
        }

        private List<string> GetErrors(Budget item, ConfigurationPath configPath)
        {
            return new List<string>();
        }

        private List<string> GetErrors(Client item, ConfigurationPath configPath)
        {
            var errors = new List<string>();

            if (!_timeTrackerClients.Any(x => x.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                errors.Add("Client is not found in the TimeTracker.");
            }

            if (item.JobDefinition == null || !item.JobDefinition.Any())
            {
                errors.Add("There are no elements in the JobDefinition.");
            }

            return errors;
        }

        private void AddMonthDuplicationsError(IEnumerable<MonthAmount> items, string propertyName, ref List<string> errors)
        {
            var monthDuplicates = items.GroupBy(x => x.GetMonthParsed())
                .Where(x => x.Count() > 1);
            if (monthDuplicates.Any())
            {
                errors.Add($"There are months duplicates in the {propertyName}: " +
                           $"{string.Join(", ", monthDuplicates.Select(x => new DateTime(2000, x.Key, 1).ToString("MMMM"))) }.");
            }
        }

        private List<string> GetErrors(JobDefinition item, ConfigurationPath configPath)
        {
            var errors = new List<string>();

            if (item.TotalHours == null || !item.TotalHours.Any())
            {
                errors.Add("There are no elements in the TotalHours.");
            }
            else
            {
                AddMonthDuplicationsError(item.TotalHours, "TotalHours", ref errors);
            }

            return errors;
        }

        private List<string> GetErrors(User item, ConfigurationPath configPath)
        {
            var errors = new List<string>();

            if (!_timeTrackerUsers.Any(x => x.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                errors.Add($"User is not found in the TimeTracker.");
            }

            return errors;
        }

        private List<string> GetErrors(JobFunction item, ConfigurationPath configPath)
        {
            var errors = new List<string>();

            var jobDefinition = configPath.Client.JobDefinition;
            if (!jobDefinition.Any(j => j.Name.Equals(item.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                errors.Add($"Job Function is not in the Job Definition.");
            }

            if (item.PercentageOfTime == null || !item.PercentageOfTime.Any())
            {
                errors.Add("There are no elements in the PercentageOfTime.");
            }
            else
            {
                AddMonthDuplicationsError(item.PercentageOfTime, "PercentageOfTime", ref errors);
            }

            return errors;
        }
    }
}
