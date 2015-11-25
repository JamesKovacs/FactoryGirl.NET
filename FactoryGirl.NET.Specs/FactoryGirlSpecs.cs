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
                It should_assign_the_default_value_for_the_property = () => builtObject.Id.ShouldEqual(Dummy.DefaultValue);

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_build_a_customized_object {
                Because of = () => builtObject = FactoryGirl.Build<Dummy>(x => x.Id = 42);
                It should_update_the_specified_value = () => builtObject.Id.ShouldEqual(42);

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_set_an_objects_int_property_using_the_int_factory {
                Because of = () =>
                {
                    var firstUseOfSequencedInt = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());
                    var secondUseOfSequencedInt = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());

                    builtObject = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());
                };

                It should_set_the_int_to_the_next_value_in_sequence = () => builtObject.Id.ShouldEqual(3);

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_set_an_objects_string_property_to_be_sequenced {
                Because of = () =>
                {
                    var firstUseOfSequencedString = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>());
                    var secondUseOfSequencedString = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>());

                    builtObject = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>());
                };

                It should_set_the_string_to_the_next_value_in_sequence = () => builtObject.String.ShouldEqual("3");

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_set_an_objects_string_property_to_be_sequenced_with_a_seed {
                Because of = () =>
                {
                    var firstUseOfSequencedString = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>());
                    var secondUseOfSequencedString = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>());

                    builtObject = FactoryGirl.Build<Dummy>(x => x.String = FactoryGirl.Sequence<string>("Test: "));
                };

                It should_set_the_string_to_the_next_value_in_sequence_with_the_seed_prefixed = () => builtObject.String.ShouldEqual("Test: 3");

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_reset_sequence_to_a_value {
                const int presetValue = 5;

                Because of = () =>
                {
                    FactoryGirl.ResetSequence(presetValue);

                    var firstUseOfSequencedInt = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());
                    var secondUseOfSequencedInt = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());

                    builtObject = FactoryGirl.Build<Dummy>(x => x.Id = FactoryGirl.Sequence<int>());
                };


                It should_start_counting_sequenced_value_from_the_point_we_set = () => builtObject.Id.ShouldEqual(3 + presetValue);

                static Dummy builtObject;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_try_to_sequence_an_unsequencable_type {
                Because of = () => exception = Catch.Exception(() => FactoryGirl.Sequence<Dummy>());
                It should_throw_a_UnsequenceableTypeException = () => exception.ShouldBeOfType<UnsequenceableTypeException>();

                static Exception exception;
            }

            [Subject(typeof(FactoryGirl))]
            public class When_we_try_to_seed_a_nonstring_sequenced_type {
                Because of = () => exception = Catch.Exception(() => FactoryGirl.Sequence<int>("seed"));
                It should_throw_a_UnsequenceableTypeException = () => exception.ShouldBeOfType<UnsequenceableTypeException>();

                static Exception exception;
            }
        }

        public void AfterContextCleanup() {
            FactoryGirl.ClearFactoryDefinitions();
            FactoryGirl.ResetSequence();
        }
    }
}