namespace TechnocomShared.Authentication
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T item);

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new PredicateSpecification<T>(t => left.IsSatisfiedBy(t) && right.IsSatisfiedBy(t));
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            return new PredicateSpecification<T>(t => left.IsSatisfiedBy(t) || right.IsSatisfiedBy(t));
        }

        public static Specification<T> operator !(Specification<T> specification)
        {
            return new PredicateSpecification<T>(t => !specification.IsSatisfiedBy(t));
        }

        public static bool operator true(Specification<T> specification)
        {
            return true;
        }

        public static bool operator false(Specification<T> specification)
        {
            return false;
        }
    }
}