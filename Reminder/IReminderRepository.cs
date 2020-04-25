namespace Reminder
{
    public interface IReminderRepository
    {
        void Save(IMedicationReminder posology);
    }
}