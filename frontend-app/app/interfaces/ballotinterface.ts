import BItem from "./iteminterface"

interface Ballot {
    id: number,
    ballotIssue: string,
    items: BItem[],
}

export default Ballot;