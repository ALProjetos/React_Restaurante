import React from "react";
import { ITimeDayModel } from "../Model/TimeDayModel";
import { Dropdown } from "semantic-ui-react";
import timeDayRepository from "../Repositories/TimeDayRepository";

export interface IDropdownTimeDaysState {
    timeDays: ITimeDayModel[]
}

export interface IDropdownTimeDaysProps {
    loading: boolean,
    onChangeTimeDay: ( ev: any, element: any ) => void
}

export default class DropdownTimeDays extends React.Component<IDropdownTimeDaysProps, IDropdownTimeDaysState> {
    constructor(props: any){
        super(props);

        this.state = {
            timeDays: [],
        }
    }

    componentDidMount(){
        this.loading();
    }

    async loading(){
        var allTimeDays = await timeDayRepository.GetAll();

        this.setState({
            timeDays: allTimeDays
        });
    }

    public render(){

        return(
            <Dropdown
                loading={this.props.loading}
                placeholder="Selecione uma opção de cardápio ..."
                fluid={true}
                search={false}
                selection={true}
                options={(this.state.timeDays || []).map(m => ({ key: m.id, text: m.description, value: m.id}))}
                onChange={this.props.onChangeTimeDay}
            />
        );
    }
}