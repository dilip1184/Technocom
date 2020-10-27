using System;

namespace TechnocomShared.Authentication
{
    public class PredicateSpecification<T> : Specification<T>
    {
        private readonly Predicate<T> _predicate;

        public PredicateSpecification(Predicate<T> predicate)
        {
            _predicate = predicate;
        }

        public override bool IsSatisfiedBy(T item)
        {
            return _predicate(item);
        }
    }
}