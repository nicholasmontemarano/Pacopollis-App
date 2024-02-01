"use client";

import { useState, useEffect } from "react";
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Button } from "./ui/button";
import { Label } from "./ui/label";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import { useSearchParams } from "next/navigation";
import { Progress } from "@/components/ui/progress";
import AdminItem from "@/app/interfaces/admininterface";

interface AdminProps {
    ballots: AdminItem[];
}

export const AdminView: React.FC<AdminProps> = ({ballots}: AdminProps) => {
    const [ballotList, setBallotList] = useState(ballots);

    return (
        <div>
            <div className="p-4">
                {ballotList && ballotList.map((ballot) => (
                    <Card className="w-auto" key={ballot.id}>
                        <CardContent>
                            <CardTitle className="flex justify-center p-4">{ballot.ballotIssue}</CardTitle>
                            {ballot.items.map((items) => (
                                <Label key={items.id}>{items.title}: {items.votes}
                                    <Progress value={(items.votes / ballot.totalVotes) * 100} className="w-[100%]" />
                                </Label>
                            ))}
                        </CardContent>
                    </Card>
                ))}
            </div>
        </div>
    );
};