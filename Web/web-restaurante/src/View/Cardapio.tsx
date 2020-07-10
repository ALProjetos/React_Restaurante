import React from "react";
import { Segment, Loader, Dimmer, Table, Form, Grid } from "semantic-ui-react";
import dishesRespository from "../Repositories/DishesRepository";
import { IDishModel } from "../Model/DishModel";
import DropdownTimeDays from "../Component/DropdownTimeDays";

export interface ICardapioState{
    loading: boolean,
    dishesModel: IDishModel[]
}

export default class Cardapio extends React.Component<{}, ICardapioState> {
    constructor(props: any){
        super(props);

        this.state = {
            loading: false,
            dishesModel: []
        }
    }

    public onChangeTimeDay = async (ev: any, element: any) => {
        this.setState({
            loading: true
        })

        let dishesModel: IDishModel[] = await dishesRespository.GetAllByTimeDayId(element.value);

        this.setState({
            ...this.state,
            loading: false,
            dishesModel: dishesModel || []
        });
    }

    public sortListDishes = (dish1: IDishModel, dish2: IDishModel): number =>{
        return((dish1.dishTypeId > dish2.dishTypeId) ? 1 : (dish1.dishTypeId < dish2.dishTypeId ? -1 : 0));
    }

    public render(){
        const dishes = (this.state.dishesModel || []).sort(this.sortListDishes);

        return(
            <Segment>
                <Dimmer active={this.state.loading} inverted >
                    <Loader content="Loading"/>
                </Dimmer>
                <Form>
                    <Grid>
                        <Grid.Column width={7}>
                            <DropdownTimeDays
                                loading={this.state.loading}
                                onChangeTimeDay={this.onChangeTimeDay.bind( this )}
                            />
                        </Grid.Column>
                    </Grid>
                </Form>
                <Table color={dishes.length > 0 ? "green" : undefined} celled={true} columns="2">
                    { dishes.length > 0
                        ? <Table.Header>
                            <Table.Row>
                                <Table.HeaderCell>Tipo de prato</Table.HeaderCell>
                                <Table.HeaderCell>Comida</Table.HeaderCell>
                            </Table.Row>
                        </Table.Header>
                        : null
                    }
                    <Table.Body>
                        { dishes.map( obj =>
                            <Table.Row key={obj.dishType}>
                                <Table.Cell>{obj.dishType}</Table.Cell>
                                <Table.Cell>{obj.dish}</Table.Cell>
                            </Table.Row>
                        ) }
                    </Table.Body>
                </Table>
            </Segment>
        )
    }
}