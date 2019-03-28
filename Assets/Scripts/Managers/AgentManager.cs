using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AgentManager : MonoBehaviour {

    public static AgentManager current;

    // delegates
    public delegate bool AgentPredicate(Agent agent);

    // references
    public List<Agent> staticallyLinkedAgents = new List<Agent>();
    
    protected Dictionary<Agent.Type, List<Agent>> agentDict = new Dictionary<Agent.Type, List<Agent>>();
    
    public AgentManager() {
        current = this;
    }

    protected void Awake() {
        AddStaticallyLinkedAgents();
    }

    protected void Start() {
        
    }

    protected void Update() {
        
    }
    
    protected void AddStaticallyLinkedAgents() {
        foreach (var agent in staticallyLinkedAgents) {
            AddAgent(agent);
        }
    }

    public void AddAgent(Agent agent) {
        EnsureTypeDictExists(agent.type);
        agentDict[agent.type].Add(agent);
        agent.onDeath += AgentOnDeath;
        OffscreenIndicatorSystem.current.AddMarker(agent.gameObject);
    }

    private void AgentOnDeath(Agent agent) {
        agentDict[agent.type].Remove(agent);
        OffscreenIndicatorSystem.current.RemoveMarker(agent.gameObject);
    }

    private void EnsureTypeDictExists(Agent.Type type) {
        if (!agentDict.ContainsKey(type)) {
            agentDict.Add(type, new List<Agent>());
        }
    }

    public static bool AgentInRange(Agent agent, Vector3 position, float radius) {
        return (agent.transform.position - position).sqrMagnitude <= radius * radius;
    }

    public void RemoveAgent(Agent agent) {
        agentDict[agent.type].Remove(agent);
    }

    public Agent FindClosestAgentTo(Vector3 position, Agent.Type type, AgentPredicate predicate = null) {
        EnsureTypeDictExists(type);

        float bestDistance = Mathf.Infinity;
        Agent result = null;
        foreach (var agent in agentDict[type]) {
            
            if (predicate != null && !predicate(agent)) continue;
            
            float distance = (agent.transform.position - position).sqrMagnitude;
            if (agent.gameObject.activeInHierarchy && distance < bestDistance) {
                bestDistance = distance;
                result = agent;
            }
        }

        return result;
    }

}

