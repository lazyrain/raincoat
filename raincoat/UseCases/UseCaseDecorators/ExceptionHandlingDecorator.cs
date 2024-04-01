namespace raincoat.UseCases.UseCaseDecorators
{
    public class ExceptionHandlingDecorator<T1, T2> : IUseCase<T1, T2>
        where T1 : IInputPack
        where T2 : IOutputPack
    {
        private readonly IUseCase<T1, T2> useCase;

        public ExceptionHandlingDecorator(IUseCase<T1, T2> useCase)
        {
            this.useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        }

        public T2 Execute(T1 input)
        {
            try
            {
                return useCase.Execute(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return default;
        }
    }
}
