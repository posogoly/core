using System;

namespace Reminder
{
    public interface IReminder
    {
        void SetReminder(IMedicationReminder posology);
    }
}
