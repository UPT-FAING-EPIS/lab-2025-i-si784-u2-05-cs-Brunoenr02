using Bank.Domain;
using System;
using Xunit;

namespace Bank.Domain.Tests
{
    public class BankAccountTests
    {
        [Theory]
        [InlineData(11.99, 4.55, 7.44)]
        [InlineData(12.3, 5.2, 7.1)]
        public void MultiDebit_WithValidAmount_UpdatesBalance(double beginningBalance, double debitAmount, double expected)
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            account.Debit(debitAmount);
            Assert.Equal(Math.Round(expected, 2), Math.Round(account.Balance, 2));
        }

        [Fact]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 11.99);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(-100));
            Assert.Contains(BankAccount.DebitAmountLessThanZeroMessage, ex.Message);
        }

        [Fact]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 11.99);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(20));
            Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, ex.Message);
        }

        [Fact]
        public void Debit_WhenAmountIsZero_ShouldNotChangeBalance()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 10);
            account.Debit(0);
            Assert.Equal(10, account.Balance);
        }

        [Fact]
        public void Debit_WhenAmountIsEqualToBalance_ShouldSetBalanceToZero()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 10);
            account.Debit(10);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void MultipleCreditsAndDebits_ShouldCalculateCorrectBalance()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 100);
            account.Credit(50);
            account.Debit(20);
            account.Credit(30);
            account.Debit(60);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Debit_WhenAmountIsNegative_ShouldThrowArgumentOutOfRange_WithMessage()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 50);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(-1));
            Assert.Contains(BankAccount.DebitAmountLessThanZeroMessage, ex.Message);
        }

        [Fact]
        public void Debit_WhenAmountExceedsBalance_ShouldThrowArgumentOutOfRange_WithMessage()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 50);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(60));
            Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, ex.Message);
        }

        // Tests para Credit

        [Fact]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange_WithMessage()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 10);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-5));
            Assert.Contains(BankAccount.CreditAmountLessThanZeroMessage, ex.Message);
        }

        [Fact]
        public void Credit_WhenAmountIsZero_ShouldNotChangeBalance()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 10);
            account.Credit(0);
            Assert.Equal(10, account.Balance);
        }

        // Test extra para Credit con monto positivo para asegurar cobertura total
        [Fact]
        public void Credit_WhenAmountIsPositive_ShouldIncreaseBalance()
        {
            BankAccount account = new BankAccount("Mr. Bryan Walton", 10);
            account.Credit(15);
            Assert.Equal(25, account.Balance);
        }
        
        [Fact]
        public void CustomerName_ShouldReturnCorrectName()
        {
            string expectedName = "Mr. Bryan Walton";
            BankAccount account = new BankAccount(expectedName, 100);
            Assert.Equal(expectedName, account.CustomerName);
        }
    }
}