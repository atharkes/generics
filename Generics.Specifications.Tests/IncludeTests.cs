using AutoFixture.Xunit2;
using FluentAssertions;
using Generics.Specifications.Enumerable;
using Generics.Specifications.Extensions;

namespace Generics.Specifications.Tests {
    public class IncludeTests {
        public sealed class ClassA {
            public IEnumerable<ClassB> SubItems { get; set; }
        }

        public sealed class ClassB {
            public int Value { get; set; } = Random.Shared.Next();
        }

        [Theory, InlineAutoData]
        public void IncludeDoesntThrow(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItems));

            var result = items.Apply(specification);
        }


        [Theory, InlineAutoData]
        public void OrderByInInclude(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItems.OrderBy(b => b.Value)));

            var result = items.Apply(specification);

            foreach (var item in result) {
                item.SubItems.Should().BeInAscendingOrder(b => b.Value);
            }

        }
    }
}