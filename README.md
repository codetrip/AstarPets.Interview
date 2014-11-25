Astar Pets Interview Test
=========================

Part 1 - Coding
---------------
**Setup:**
Open AstarPets.Interview\AstarPets.Interview.sln

**Scenario:**
The existing AstarPets.Interview shopping cart (hit F5 in Visual Studio to see it) allows you to add and remove items. As you add each item you can set its Shipping method.
The available shipping options are stored in App_Data\Shipping.xml.

**Objectives:**
1. Add a new Shipping Option to the application code. The Shipping Option should behave the same as the PerRegion Option except that if there is at least one other item in the basket with the same Shipping Option and the same Supplier and Region, 50p should be deducted from the shipping. Keep in mind the ability to change the parameters.
2. Write a unit test for the new code.
3. Form some opinions about how the code has been put together for discussion. Note that we are not looking for any particular criticisms (there are no "trick" mistakes, though there may be some genuine ones!).
4. When done, get the code back to us.  Bonus points for a pull request, but if you're new to DVCS don't worry - just zip up the code and email it.

**Time allowed:**
There is no time limit, but hopefully it shouldn't take you longer than about an hour.

**Tips:**
1. App_Data\Shipping.xml is created with the CreateSampleData unit test. You should add to this to create your extended version of this file.
2. Don't forget to add any new Shipping types to the KnownTypes method on ShippingBase.
3. The Controller action used for the page is the Index action on the HomeController.


Part 2 - Design
---------------
We'd like the Shipping App to use the Unit of Work pattern.  For the purposes of this assignment, the Unit of Work pattern means:
The framework takes care of persisting all data at the end of a defined "unit of work".
In concrete terms, as the Shipping App is a web app, a "unit of work" will be a web request.  This means that the application code should not be explicitly saving the basket after modification, but instead the framework should somehow know that it has changed and save it at the end of the web request.
Please produce a document explaining how you might do this, including any restrictions your solution has or considerations as the app continued to be developed.  Your document can include diagrams, pseudo-code or just consist of words.   
Note that the important thing is your ideas and thinking and how you get them across, not the presentation / formatting of the document, so please don't worry unduly about this.

Any questions about either part of this assignment, please don't hesitate to ask.  Good luck!
