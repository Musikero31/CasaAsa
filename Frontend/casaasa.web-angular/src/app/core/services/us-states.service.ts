import { Injectable } from "@angular/core";
import states from "../../../assets/data/us-states.json";
import { USStateModel } from "../../shared/models/us-state.model";

@Injectable({
    providedIn: 'root'
})
export class USStatesService {
    getStates() : USStateModel[] {
        return states as USStateModel[];
    }
}
