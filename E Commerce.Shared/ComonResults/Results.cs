

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Shared.ComonResults
{
    public class Results
    {
        protected readonly List<Errors> _errors = [];
        public bool IsSuccess => _errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Errors> Errors => _errors;

        #region Results constructors

        // IsSuccess constructor Result OK 200  
        protected Results()
        {
        }
        // IsFailure constructor Result With one Error
        protected Results(Errors error)
        {
            _errors.Add(error);
        }
        // IsFailure constructor Result With multiple Errors
        protected Results(List<Errors> errors)
        {
            _errors.AddRange(errors);
        }
        #endregion

        public static Results Ok() => new Results();
        public static Results Fail(Errors error) => new Results(error);
        public static Results Fail(List<Errors> errors) => new Results(errors);
    }
    public class Results<T> : Results
    {
        private readonly T _value;
        public T Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");
        #region Results<T> constructors
        // IsSuccess constructor Result<T> OK 200  
        private Results(T value)
        {
            _value = value;
        }
        // IsFailure constructor Result<T> With one Error
        private Results(Errors error) : base(error)
        {
            _value = default!;
        }
        // IsFailure constructor Result<T> With multiple Errors
        private Results(List<Errors> errors) : base(errors)
        {
            _value = default!;
        }
        #endregion
        public static Results<T> Ok(T value) => new Results<T>(value);
        public static new Results<T> Fail(Errors error) => new Results<T>(error);
        public static new Results<T> Fail(List<Errors> errors) => new Results<T>(errors);
        #region implicit operator
        public static implicit operator Results<T>(T value) => Ok(value);
        public static implicit operator Results<T>(Errors error) => Fail(error);
        public static implicit operator Results<T>(List<Errors> errors) => Fail(errors);
        #endregion
    }
}
