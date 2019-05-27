using LedgerKit;
using Xunit;

namespace LedgerKit.Tests
{
    public class AccountTests
    {
        [Fact]
        public void Account_Requires_Name()
        {
            Assert.Throws<System.ArgumentException>(() => new Account("", AccountType.Asset));
        }

        [Fact]
        public void Account_Equality_Is_By_Value()
        {
            var a = new Account("Cash", AccountType.Asset);
            var b = new Account("Cash", AccountType.Asset);
            Assert.Equal(a, b);
        }
        // MARKER: account-tests
    }
}
