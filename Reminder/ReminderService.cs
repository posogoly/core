namespace Reminder
{
    public class ReminderService:IReminder
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public void SetReminder(IMedicationReminder posology)
        {
            _reminderRepository.Save();
        }
    }
}