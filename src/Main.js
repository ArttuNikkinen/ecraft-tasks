import React from 'react';
import SplitPane, { Pane } from 'react-split-pane';
import CurrentMeeting from './components/CurrentMeeting';
import FeaturedMeetings from './components/FeaturedMeetings';
import ScheduleView from './components/ScheduleView';

//This is the Main component. Each component is called from this component.



class Main extends React.Component {
    render() {
        return (
            <div className="Main">
                <SplitPane>
                    <Pane className="Pane1">
                        <CurrentMeeting />
                        <FeaturedMeetings />
                    </Pane>
                    <Pane className="Pane2">

                        <ScheduleView />
                    </Pane>
                </SplitPane>
            </div>
        )
    }
}
export default Main;