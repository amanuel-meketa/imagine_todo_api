namespace Imagine_todo.application.Exceptions
{

        public class ApplicationException : Exception
        {
            public ApplicationException(string message) : base(message)
            {}
        }
        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message)
            {}
        }
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            {}
        }
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            {}
        }
        public class ForbiddenException : Exception
        {
            public ForbiddenException(string message) : base(message)
            {}
        }
}
