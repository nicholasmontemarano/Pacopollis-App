import AItem from "./aiteminterface"
interface AdminItem {
    id: number,
    ballotIssue: string,
    totalVotes: number,
    items: AItem[],
}

export default AdminItem;