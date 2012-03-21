using System;
using Machine.Specifications;

namespace FactoryGirl.NET.Specs {
    [Subject(typeof(FactoryGirl))]
    public class FactoryGirlSpecs : ICleanupAfterEveryContextInAssembly {

        [Subject(typeof(FactoryGirl))]
        public class When_we_define_a_factory {
            Establish context = () => { };
            Because of = () => FactoryGirl.Define(() => new Dummy());
            It should_contain_the_definition = () => FactoryGirl.DefinedFactories.ShouldContain(typeof(Dummy));
        }

        [Subject(typeof(FactoryGirl))]
        public class When_a_factory_has_been_defined {
            Establish context = () => FactoryGirl.Define(() => new Dummy());

            [Subject(typeof(FactoryGirl))]
            public class When_we_define_the_same_factory_again {
                Because of = () => exception = Catch.Exception(() => FactoryGirl.Define(() => new Dummy()));
                It should_throw_a_DuplicateFactoryException = () => exception.ShouldBeOfType<DuplicateFactoryException>();

                static Exception exception;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_build_a_default_object {
                Because of = () => builtObject = FactoryGirl.Build<Dummy>();
                It should_build_the_object = () => builtObject.ShouldNotBeNull();
                It should_assign_the_default_value_for_the_property = () => builtObject.Value.ShouldEqual(Dummy.DefaultValue);

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_build_a_customized_object {
                Because of = () => builtObject = FactoryGirl.Build<Dummy>(x => x.Value = 42);
                It should_update_the_specified_value = () => builtObject.Value.ShouldEqual(42);

                static Dummy builtObject;
            }
        }

        public void AfterContextCleanup() {
            FactoryGirl.ClearFactoryDefinitions();
        }
    }
}