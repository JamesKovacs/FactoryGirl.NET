FactoryGirl.NET
===============
Minimal implementation of Ruby's [factory\_girl](http://github.com/thoughtbot/factory_girl) in .NET.
If you are familiar with factory\_girl in Ruby, we only support defining factories and building objects
using those factories. We do not support create (e.g. saving to the database), attributes\_for, or 
build\_stubbed yet.

To define a factory:

    FactoryGirl.Define(() => new User {
                                  FirstName = "John",
                                  LastName = "Doe",
                                  Admin = false
                                });

To use a factory:

    var user = FactoryGirl.Build<User>();

To customize the object being built:

    var admin = FactoryGirl.Build<User>(x => x.Admin = true);

Copyright &copy; 2012 [James Kovacs](http://jameskovacs.com)
