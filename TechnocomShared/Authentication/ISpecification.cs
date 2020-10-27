namespace TechnocomShared.Authentication
{
    internal interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}