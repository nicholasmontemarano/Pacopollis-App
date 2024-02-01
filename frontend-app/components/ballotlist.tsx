"use client";

import { useEffect, useState } from "react";

import axios from "axios";
import Ballot from '@/app/interfaces/ballotinterface'
import Vote from '@/app/interfaces/votesinterface';
import { Button } from "./ui/button";
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import { Label } from "./ui/label";
import { useSearchParams } from "next/navigation";

interface BallotProps {
    ballots: Ballot[];
}

export const BallotList = ({ ballots }: BallotProps) => {
    const searchParams = useSearchParams();
    const [electionID, setElectionID] = useState(searchParams.get("electionid"));
    const [ballotList, setBallotList] = useState(ballots);
    const [voteList, setVoteList] = useState<Vote[]>([]);

    const onSubmit = async (e: React.FormEvent) => { // TODO fix so that it sends it to the server
        e.preventDefault();
        console.log(voteList);
        var voteString: string = "";
        voteList.forEach((vote: Vote) => {
            voteString = voteString + vote.itemID + ",";
            console.log(voteString);
        })
        voteString = voteString.substring(0, voteString.length - 1);
        console.log(voteString);

        const loggedInUser = sessionStorage.getItem('user');

        const axios = require('axios');

        let config = {
            method: 'post',
            maxBodyLength: Infinity,
            url: 'http://localhost:5014/api/SendVotes?voter_id=' + loggedInUser +  '&election_id='+ electionID +'&votes=' + voteString,
            headers: {}
        };

        axios.request(config)
            .then((response) => {
                console.log(JSON.stringify(response.data));
            })
            .catch((error) => {
                console.log(error);
            });

        location.href = "http://localhost:3000/dashboard";
    };

    const handleVotes = async (value: string) => {
        const values: string[] = value.split(",");
        const votes: Vote[] = [];
        var x = 0;
        ballotList.forEach((ballot) => {
            x++;
            const newVote: Vote = {
                voteID: x,
                ballotID: ballot.id,
                ballotIssue: ballot.ballotIssue,
                itemID: parseInt(values[0]),
                itemTitle: values[1],
            }
            votes.push(newVote);
            setVoteList(votes);
        });
        console.log(votes);
    }

    return (
        <div>
            <form>
                {ballotList.map((ballot) => (
                    <div className="p-2">
                        <Card key={ballot.id}>
                            <CardContent>
                                <CardTitle className="p-4">{ballot.ballotIssue}</CardTitle>
                                <RadioGroup onValueChange={(value) => handleVotes(value)}>
                                    {ballot.items.map((item) => (
                                        <div key={item.id} className="flex items-center space-x-2">
                                            <RadioGroupItem value={item.id.toString() + "," + item.title} />
                                            <Label>{item.title}</Label>
                                        </div>
                                    ))}
                                </RadioGroup>
                            </CardContent>
                        </Card>
                    </div>
                ))}
                <div className="flex justify-center">
                    <AlertDialog>
                        <AlertDialogTrigger asChild>
                            <Button className="px-20 py-8 text-lg">Submit</Button>
                        </AlertDialogTrigger>
                        <AlertDialogContent>
                            <AlertDialogHeader>
                                <AlertDialogTitle>Review Your Votes</AlertDialogTitle>
                                <AlertDialogDescription>
                                    <Table>
                                        <TableHeader>
                                            <TableRow>
                                                <TableHead className="w-[100px]">Item</TableHead>
                                                <TableHead className="text-right">Vote</TableHead>
                                            </TableRow>
                                        </TableHeader>
                                        {voteList.map((ballot) => (
                                            <TableBody key={ballot.ballotID}>
                                                <TableRow>
                                                    <TableCell className="font-medium">{ballot.ballotIssue}</TableCell>
                                                    <TableCell className="text-right">{ballot.itemTitle}</TableCell>
                                                </TableRow>
                                            </TableBody>
                                        ))}
                                    </Table>
                                </AlertDialogDescription>
                            </AlertDialogHeader>
                            <AlertDialogFooter>
                                <AlertDialogCancel>Cancel</AlertDialogCancel>
                                <AlertDialogAction asChild>
                                    <Button onClick={onSubmit}>
                                        Continue
                                    </Button>
                                </AlertDialogAction>
                            </AlertDialogFooter>
                        </AlertDialogContent>
                    </AlertDialog>
                </div>
            </form >
        </div>
    );
};