# SimpleRiskApplication

NOTES

I've not included the Nuget Packages. Vis Stu should just download them automatically?
This is probably about 4-5 hours in total, it'll take me a couple of weeks to get back upto speed. (The config namespace used up about 1.5 hours! That stuff is really fiddly and I don't have any previous code to reference)

TODO

1 - A rough crack, still needs the rest of the application-rules applied. (I assumed they were app logic rather than business logic)

2 - Forcing changes to observable collections onto the UI Thread which isn't great.

3 - Needs loads of unit testing

4 - UI elements are not virtualised so a large dataset might use a lot of memory.

5 - Nothing on the UI to show how much of the data has been loaded (progress bar etc..)

DONE

1 - Files are loaded async on their own threads and the data trickles up to the UI in batches, the UI should update as the data is recieved.

2 - I've used icons in columns to show the app rules, doesn't look great but it's a proof of concept.

Any questions feel free to drop me an email.
