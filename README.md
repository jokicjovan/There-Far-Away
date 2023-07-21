<img src="https://github.com/jokicjovan/There-Far-Away/assets/51921035/904a7bfe-b5f7-4d3f-bc78-323a4b2472e3" width="300">
<br/>
<img src="https://github.com/jokicjovan/There-Far-Away/assets/51921035/f077e98a-f739-4a28-8107-7283cffc06cf" width="330">


# There-Far-Away

There Far Away is a  WPF application tailored for a domestic travel agency in Serbia. This application adopts the MVVM (Model-View-ViewModel) architectural pattern and incorporates Material Design principles for a modern and user-friendly interface. Specifically designed for a 41-year-old woman with basic domain knowledge and computer proficiency, the app caters to her need for speed, efficiency, and automation, even in a distracted and time-limited work environment.

To optimize usability on small-screen Netbook-like displays with a resolution of 750x430 and 96dpi, There Far Away is thoughtfully adapted for compact screens. The app's responsive design ensures seamless interactions and easy navigation, providing an exceptional user experience.

Two distinct user roles are supported: Users and Agents. Agents, with administrative privileges, can effortlessly manage journeys and attractions. They can add, edit, and delete journeys and attractions, as well as combine multiple attractions into comprehensive packages. Additionally, Agents have access to various reports, facilitating data analysis and decision-making.

On the other hand, Users can register, explore available journeys and attractions, and proceed to purchase or reserve their preferred trips. The app streamlines these processes, incorporating shortcuts and automation to minimize user effort and maximize efficiency. <br/>

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/cd622359-b7a1-42c8-b064-d7a5008ed734)

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/1d1ab5ff-5173-47ca-becc-a4134ca87a49)

## Motivation
Our project motivation is born from a profound love and dedication to our homeland. As a team of three individuals, we share a common bond of cherishing our faith, church, and tradition, which form the core of our cultural and religious identity. For us, Kosovo and Metohija hold a special significance as the heart and soul of our country.

The sacrifices made by our ancestors in defense of Serbia and Kosovo inspire us to carry forward their legacy. In a world where our roots are gradually being alienated, we strive to protect and preserve our invaluable heritage. Our mission is to ensure that our faith, tradition, and love for the motherland remain steadfast and nurtured for generations to come.

Through our work on this project, we aim to create a platform that celebrates the essence of our homeland. We want to instill a sense of pride and appreciation for our cultural heritage, fostering a deeper connection with our roots. Our project is fueled by a shared passion for safeguarding our identity and promoting the beauty and richness of our homeland to the world.

# Features
## Journeys
### Preview
Within the There Far Away WPF application, Agents are equipped with a fast and efficient preview of all touristic journeys through a visually appealing card-based interface. Each journey is presented as a compact card displaying its name and the start and end dates, allowing Agents to quickly scan and identify relevant details at a glance.

This card-based view is thoughtfully designed for the small-screen Netbook-like displays, ensuring that Agents can seamlessly navigate through multiple journeys without feeling overwhelmed by excessive information.

For a more comprehensive understanding of a specific journey, Agents can easily access detailed information with a single click. A dedicated "Detailed View" button is provided on each journey card, enabling Agents to delve deeper into the journey's itinerary, activities, pricing, and other essential aspects.
<br/>
![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/1b42ce74-71bd-41da-9b68-f80dfbd07022)

### Create/Edit Journey
The Agent feature for adding journeys in the There Far Away WPF application offers a streamlined and user-friendly process through a well-organized three-step Stepper. This design caters to the Agent's work environment, characterized by distractions, limited time, and the need for efficiency.

Step 1: Journey Information
On the first page of the Stepper, the Agent inputs essential journey details, such as the journey name, start and end dates, price, type of transport, and an eye-catching cover image. To ensure data accuracy, the Agent cannot proceed to the next page until all inputs are validated, preventing any potential errors due to the distracted nature of their work environment.

Step 2: Journey Location Selection
The second page features a map view, where the Agent can specify the journey's start and end locations. Two inputs are available for the Agent to manually type addresses, and upon pressing Enter, they receive real-time feedback and suggestions. Alternatively, Agents can use an interactive icon next to the input to place a pin on the map, simplifying the selection process. This intuitive approach minimizes data entry errors and enhances the user experience.

Step 3: Attractions Selection
The third page of the Stepper offers two searchable lists. The first list comprises all available attractions, and the second consists of selected attractions for the journey. Agents can effortlessly add attractions by utilizing a drag-and-drop mechanism between the lists. Additionally, the map of Serbia displays pins for the selected attractions, providing a visual representation of their locations. This feature aids the Agent in crafting engaging and diverse journey itineraries while maintaining a clear overview.

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/10e39dd0-15aa-4a18-8b3b-4a9167fd8c99)
![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/d2dac7af-8ff8-456b-8248-d56c0bf5c6b0)
![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/84330bdd-a194-4a7b-8e67-59517f0ca48a)

### Detailed Preview
The Detailed Journey Preview in the There Far Away WPF application provides a comprehensive overview of a specific journey. The page displays crucial journey details, including the start and end dates, pricing, and a list of captivating attractions awaiting travelers.

For a deeper understanding of the attractions, the page offers an "Info" button next to each attraction. Clicking on this button reveals detailed information about the attraction, allowing travelers to make informed decisions and get a glimpse of the exciting experiences that await them.

Furthermore, a dynamic map showcases the journey's route, offering an interactive and visual representation of the itinerary. The map helps travelers visualize the travel route, attractions' locations, and the journey's overall geography.

For Administrators, additional functionality is available at the bottom of the page. Three buttons provide convenient access to editing the journey, deleting it if necessary, and viewing a comprehensive list of passengers who have purchased the journey. This empowers administrators to manage journeys efficiently and respond promptly to any changes or inquiries.

On the other hand, in the passenger view of the page, users have the option to buy the journey or reserve/cancel their registration. This seamless functionality enables travelers to easily secure their spot or make changes as needed.
![2023-07-21 19 03 08](https://github.com/jokicjovan/There-Far-Away/assets/51921035/63a5594f-6ecd-4579-8017-270b6c16f040)

### Reports
This report displays the number of sold journeys for each specific journey offered by the travel agency.By presenting this data in a clear and structured format, the Journey Report empowers administrators to quickly assess the popularity and demand for each journey. This valuable insight helps them make informed business decisions, optimize marketing strategies, and ensure efficient allocation of resources.

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/434cfc08-d9bc-412b-9e4d-999238de0563)

## Attractions
### Preview
In the There Far Away WPF application, Agents can efficiently preview touristic attractions through a visually appealing card-based interface. Each card displays the attraction's name and type (accommodation, restaurant, attraction). Agents can access detailed information by clicking the "Detailed View" button on the card, empowering them to make informed decisions in their busy and time-limited work environment.
![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/36559e21-61bf-4965-8487-7dc9e5de445c)

### Create/Edit Attraction
The Agent feature for adding attractions in the There Far Away WPF application streamlines the process through a two-step Stepper. This design is tailored for Agents who work in a distracted environment and require speed and accuracy in their tasks.

Step 1: Attraction Information
On the first page of the Stepper, the Agent inputs essential attraction details, including the name, type (e.g., accommodation, restaurant, attraction), description, and an eye-catching cover image. The application ensures data validity, and the Agent cannot proceed to the next page until all inputs are complete and validated, preventing errors in their fast-paced work environment.

Step 2: Attraction Location Selection
The second page offers an interactive map view for selecting the attraction's location. Agents can manually type the address in the input field and receive real-time feedback and suggestions by pressing Enter. Alternatively, Agents can use an interactive icon next to the input to place a pin on the map, simplifying the selection process. This intuitive approach minimizes data entry errors and enhances the Agent's productivity.

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/5cc0fa40-702b-475e-adc4-9a48171d8f7d)
![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/4511b5fe-f4f6-47aa-b718-cdbc3ebb4389)

### Detailed Preview

The page displays essential information, including the attraction's name, type (e.g., accommodation, restaurant, attraction), and a detailed description. The attractive and user-friendly interface ensures easy readability and quick access to attraction details.

For an enhanced user experience, the Detailed Attraction Preview also includes a dynamic map displaying the attraction's location. This interactive map feature allows users to visualize the attraction's geographic position, facilitating better planning and decision-making.

For Administrators, two convenient buttons are available. The first button enables administrators to edit the attraction, granting them the ability to make updates and improvements as needed. The second button allows for the easy deletion of the attraction, ensuring a straightforward process for managing and maintaining the attraction database.

![image](https://github.com/jokicjovan/There-Far-Away/assets/51921035/40c91d82-7d04-4b90-bd0c-5c3ef759336f)


## Authors

- [Jovan Jokić](https://github.com/jokicjovan)
- [Bojan Mijanović](https://github.com/bmijanovic)
- [Vukašin Bogdanović](https://github.com/vukasinb7)
