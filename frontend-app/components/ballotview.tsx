"use client";

import { useEffect, useState } from "react";
import Vote from "@/app/interfaces/votesinterface";
import { Button } from "./ui/button";

import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import Link from "next/link";

interface VoteProps {
    votes: Vote[];
}

export const BallotView = ({ votes }: VoteProps) => {
    const [voteList, setVoteList] = useState(votes);
    return (
        <div>
            {voteList.map((ballot) => (
                <div className="p-2">
                    <Card key={ballot.voteID}>
                        <CardContent>
                            <CardTitle className="p-4">{ballot.ballotIssue}</CardTitle>
                            <CardDescription>{ballot.itemTitle}</CardDescription>
                        </CardContent>
                    </Card>
                </div>
            ))}
        </div>
    )
};