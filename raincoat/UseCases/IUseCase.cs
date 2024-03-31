namespace raincoat.UseCases
{
    public interface IUseCase<in T1, out T2>
        where T1 : IInputPack
        where T2 : IOutputPack
    {
        T2 Execute(T1 input);
    }
}
