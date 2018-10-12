using NUnit.Framework;

namespace Phnx.Collections.Tests.Tree
{
    public class TreeTests
    {
        [Test]
        public void CreatingANewTree_WithNoEntries_HasCountZero()
        {
            // Arrange
            var tree = new Tree<object>();

            // Assert
            Assert.AreEqual(0, tree.TotalNodes);
            Assert.AreEqual(0, tree.TotalNodeRelationships);
        }

        [Test]
        public void CreatingATree_WithTwoEntries_HasCountTwoAndNoRelationships()
        {
            // Arrange
            var tree = new Tree<object>();

            // Act
            tree.AddTopNode(new object());
            tree.AddTopNode(new object());

            // Assert
            Assert.AreEqual(2, tree.TotalNodes);
            Assert.AreEqual(0, tree.TotalNodeRelationships);
        }

        [Test]
        public void CreatingATree_WithOneParentOneChild_HasCountTwoAndOneRelationship()
        {
            // Arrange
            var tree = new Tree<object>();

            // Act
            var topNode = tree.AddTopNode(new object());
            topNode.AddChild(new object());

            // Assert
            Assert.AreEqual(2, tree.TotalNodes);
            Assert.AreEqual(1, tree.TotalNodeRelationships);
        }

        [Test]
        public void CreatingATree_WithThreeLayersAndDuplicateNodes_HasCorrectCountAndRelationships()
        {
            // Arrange
            var tree = new Tree<object>();

            // Act
            var top1 = tree.AddTopNode(new object());

            var top1mid1 = top1.AddChild(new object());

            var top1mid1bot1 = top1mid1.AddChild(new object());
            var top1mid1bot2 = top1mid1.AddChild(new object());
            var top1mid1bot3 = top1mid1.AddChild(new object());

            top1.AddChild(top1mid1);
            top1.AddChild(top1mid1);

            var top2 = tree.AddTopNode(new object());
            top2.AddChild(top1mid1);
            top2.AddChild(top1mid1);
            top2.AddChild(top1mid1);

            var top3 = tree.AddTopNode(new object());
            top3.AddChild(top1mid1);
            top3.AddChild(top1mid1);
            top3.AddChild(top1mid1);

            // Assert
            Assert.AreEqual(7, tree.TotalNodes);
            Assert.AreEqual(12, tree.TotalNodeRelationships);
        }

        [Test]
        public void CreatingATree_WithSingleSelfReferencingNode_ReturnsCorrectCountAndRelationships()
        {
            // Arrange
            var tree = new Tree<object>();

            // Act
            var top1 = tree.AddTopNode(new object());

            top1.AddChild(top1);

            // Assert
            Assert.AreEqual(1, tree.TotalNodes);
            Assert.AreEqual(1, tree.TotalNodeRelationships);
        }

        [Test]
        public void CreatingATree_WithReferenceLoopTree_ReturnsCorrectCountAndRelationships()
        {
            // Arrange
            var tree = new Tree<object>();

            // Act
            var top1 = tree.AddTopNode(new object());

            var child = top1.AddChild(new object());
            child.AddChild(top1);

            var subChild = child.AddChild(new object());

            subChild.AddChild(top1);
            subChild.AddChild(top1);
            subChild.AddChild(child);

            // Assert
            Assert.AreEqual(3, tree.TotalNodes);
            Assert.AreEqual(6, tree.TotalNodeRelationships);
        }
    }
}
