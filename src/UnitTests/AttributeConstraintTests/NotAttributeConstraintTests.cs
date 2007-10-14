#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

namespace WatiN.Core.UnitTests
{
  using NUnit.Framework;
  using Rhino.Mocks;
  using WatiN.Core.Interfaces;

  [TestFixture]
  public class NotAttributeConstraintTests
  {
    private MockRepository mocks;
    private AttributeConstraint attribute;
    private IAttributeBag attributeBag;

    [SetUp]
    public void Setup()
    {
      mocks = new MockRepository();
      attribute = (AttributeConstraint) mocks.DynamicMock(typeof (AttributeConstraint), "fake", "");
      attributeBag = (IAttributeBag) mocks.DynamicMock(typeof (IAttributeBag));

      SetupResult.For(attribute.Compare(null)).IgnoreArguments().Return(false);
      mocks.ReplayAll();
    }

    [TearDown]
    public void TearDown()
    {
      mocks.VerifyAll();
    }

    [Test]
    public void NotTest()
    {
      NotAttributeConstraint notAttributeConstraint = new NotAttributeConstraint(attribute);
      Assert.IsTrue(notAttributeConstraint.Compare(attributeBag));
    }

    [Test]
    public void AttributeOperatorNotOverload()
    {
      AttributeConstraint attributenot = !attribute;

      Assert.IsInstanceOfType(typeof (NotAttributeConstraint), attributenot, "Expected NotAttributeConstraint instance");
      Assert.IsTrue(attributenot.Compare(attributeBag));
    }
  }
}