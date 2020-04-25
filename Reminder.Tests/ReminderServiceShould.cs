using Xunit;
using Moq;

namespace Reminder.Tests
{
    public class ReminderServiceShould
    {
        [Fact]
        public void Save_Reminder_In_Repository()
        {
            var repo = new Mock<IReminderRepository>();
            var reminderService = new ReminderService(repo.Object);

            var posology = new MedicationReminder();
            reminderService.SetReminder(posology);

            repo.Verify(repository => repository.Save(posology));
        }
    }
}
