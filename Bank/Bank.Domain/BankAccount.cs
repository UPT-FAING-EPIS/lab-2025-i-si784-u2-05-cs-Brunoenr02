namespace Bank.Domain
{
    /// <summary>
    /// Representa una cuenta bancaria con funcionalidades para acreditar y debitar fondos.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Mensaje de error cuando el monto a debitar excede el saldo disponible.
        /// </summary>
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

        /// <summary>
        /// Mensaje de error cuando el monto a debitar es menor que cero.
        /// </summary>
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

        /// <summary>
        /// Mensaje de error cuando el monto a acreditar es menor que cero.
        /// </summary>
        public const string CreditAmountLessThanZeroMessage = "Credit amount is less than zero";

        private readonly string m_customerName;
        private double m_balance;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BankAccount"/> con el nombre del cliente y el saldo inicial.
        /// </summary>
        /// <param name="customerName">Nombre del titular de la cuenta.</param>
        /// <param name="balance">Saldo inicial de la cuenta.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Obtiene el nombre del titular de la cuenta.
        /// </summary>
        public string CustomerName { get { return m_customerName; } }

        /// <summary>
        /// Obtiene el saldo actual de la cuenta.
        /// </summary>
        public double Balance { get { return m_balance; } }

        /// <summary>
        /// Debita un monto especificado de la cuenta.
        /// </summary>
        /// <param name="amount">Monto a debitar.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se lanza cuando el monto es menor que cero o excede el saldo disponible.
        /// </exception>
        public void Debit(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountLessThanZeroMessage);

            if (amount > m_balance)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountExceedsBalanceMessage);

            m_balance -= amount;
        }

        /// <summary>
        /// Acredita un monto especificado a la cuenta.
        /// </summary>
        /// <param name="amount">Monto a acreditar.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se lanza cuando el monto es menor que cero.
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, CreditAmountLessThanZeroMessage);

            m_balance += amount;
        }
    }
}
