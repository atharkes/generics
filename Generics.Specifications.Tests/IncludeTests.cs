using AutoFixture.Xunit2;
using FluentAssertions;
using Generics.Specifications.Enumerable;
using Generics.Specifications.Extensions;

namespace Generics.Specifications.Tests {
    public class IncludeTests {
        public sealed class ClassA {
            public IEnumerable<ClassB> SubItems { get; set; }
            public ClassB SubItem { get; set; }
        }

        public sealed class ClassB {
            public IEnumerable<ClassC> SubItems { get; set; }
            public int Value { get; set; }
        }

        public sealed class ClassC {
            public uint Value { get; set; }
        }

        [Theory, InlineAutoData]
        public void IncludeDoesntThrow(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItems));

            var result = items.Apply(specification);

            result.Should().BeEquivalentTo(items);
        }

        [Theory, InlineAutoData]
        public void ThenIncludeAfterReferenceDoesntThrow(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItem).ThenInclude(b => b.SubItems));

            var result = items.Apply(specification);

            result.Should().BeEquivalentTo(items);
        }

        [Theory, InlineAutoData]
        public void ThenIncludeAfterEnumerableDoesntThrow(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItems).ThenInclude(b => b.SubItems));

            var result = items.Apply(specification);

            result.Should().BeEquivalentTo(items);
        }


        [Theory, InlineAutoData]
        public void OrderByInInclude(IEnumerable<ClassA> items) {
            var specification = new QuerySpecification<ClassA>(q => q.Include(a => a.SubItems.OrderBy(b => b.Value)));

            var result = items.Apply(specification);

            foreach (var item in result) {
                item.SubItems.Should().BeInAscendingOrder(b => b.Value);
            }
        }

        [Theory, InlineAutoData]
        public void Modify_NoOperation_SameResult(IEnumerable<ClassA> items) {
            var result = items.Modify(item => item.SubItems, subItems => subItems);

            result.Should().BeEquivalentTo(items);
        }

        [Theory, InlineAutoData]
        public void Modify_OrderBy_OrderedResult(IEnumerable<ClassA> items) {
            var result = items.Modify(item => item.SubItems, subItems => subItems.OrderBy(i => i.Value));

            foreach (var item in result) {
                item.SubItems.Should().BeInAscendingOrder(b => b.Value);
            }
        }
    }
}