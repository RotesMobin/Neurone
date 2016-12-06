using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionTextScript : MonoBehaviour {

    string[] questions;
    int[] reponses;
    List<int> questionPasse;
    public float tDelta;
    public float tQuest;
    public int nbQ;
    public int currentQuestion;
    public float t;
    public int nombreBonneReponse;

	// Use this for initialization
	void Start () {
        tDelta = 30.0f;
        tQuest = 20.0f;
        nbQ = 22;
        nombreBonneReponse = 0;

        questionPasse = new List<int>();

        questions = new string[nbQ];
        questions[0] = "5*15 = ? 75 / 125";
        questions[1] = "Quand a eu lieu la prise de la Bastille ? 1789 / 1515";
        questions[2] = "Quelle est la couleur d'un panneau de stationnement interdit ? Jaune et rouge / Bleu et rouge";
        questions[3] = "Quel nombre est le plus petit ? 6645 / 3564";
        questions[4] = "Il fait froid au Pole Nord. Vrai / Faux";
        questions[5] = "16 = ? 64/4 / 12+6";
        questions[6] = "Paul à 5€. Il mange un sandwich. Combien d'euros lui reste-t'il ? 5 / 2";
        questions[7] = "De quoi est faite la maison du 3ème petit cochon ? Brique / Ciment";
        questions[8] = "Que veut dire \"mad\" ? Fait / Fou";
        questions[9] = "Le saucisson est fait de... ? Porc / Poisson";
        questions[10] = "La Nintendo 3DS est... ? Un appareil de massage / Une console de jeu";
        questions[11] = "550 = ? 25+525 / 32-12";
        questions[12] = "3 * 3 + 3 = ? 12 / 18";
        questions[13] = "Laquelle de ces couleurs de cheveux n'est pas naturelle ? Chatain / Bleu";
        questions[14] = "Quelle est la couleur d'un tableau blanc ? Blanc / Noir";
        questions[15] = "Le téléphone a été inventé au XIème siècle. Vrai / Faux";
        questions[16] = "Quel est le nombre de faute dans cet question ? 2 / 4";
        questions[17] = "32 + 32 = ? 64 / 54";
        questions[18] = "Lequel est le plus gros ? Un éléphant / Un atome";
        questions[19] = "Une fenêtre peut être faite de... ? Verre / Vert";
        questions[20] = "Quel chiffre vient après le 2 ? 3 / 10";
        questions[21] = "Quel parasite vit dans les cheveux ? Puce / Poux";
        
        reponses = new int[nbQ];
        reponses[0] = 1;
        reponses[1] = 1;
        reponses[2] = 2;
        reponses[3] = 2;
        reponses[4] = 1;
        reponses[5] = 1;
        reponses[6] = 1;
        reponses[7] = 1;
        reponses[8] = 2;
        reponses[9] = 1;
        reponses[10] = 2;
        reponses[11] = 1;
        reponses[12] = 1;
        reponses[13] = 2;
        reponses[14] = 1;
        reponses[15] = 2;
        reponses[16] = 1;
        reponses[17] = 1;
        reponses[18] = 1;
        reponses[19] = 1;
        reponses[20] = 1;
        reponses[21] = 2;

        t = Time.time;
        currentQuestion = -1;
        gameObject.GetComponent<Text>().text = "Pour répondre, Première réponse : O, Deuxième réponse : P" ;
    }
	
	// Update is called once per frame
	void Update () {


	    if (currentQuestion != -1)
        {
            if (Time.time - t > 30.0)
            {
                t = Time.time;
                currentQuestion = -1;
                gameObject.GetComponent<Text>().text = "Temps écoulé !";
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                t = Time.time;
                if (reponses[currentQuestion] == 1)
                {
                    nombreBonneReponse++;
                    gameObject.GetComponent<Text>().text = "Bonne réponse !";
                } else
                {
                    nombreBonneReponse++;
                    gameObject.GetComponent<Text>().text = "Mauvaise réponse !";
                }
                currentQuestion = -1;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                t = Time.time;
                if (reponses[currentQuestion] == 0)
                {
                    nombreBonneReponse++;
                    gameObject.GetComponent<Text>().text = "Bonne réponse !";
                }
                else
                {
                    nombreBonneReponse++;
                    gameObject.GetComponent<Text>().text = "Mauvaise réponse !";
                }
                currentQuestion = -1;
            }
        } else
        {
            if (Time.time - t > 10.0)
            {
                t = Time.time;
                do
                {
                    currentQuestion = (int)(Random.value * nbQ);
                } while (questionPasse.Contains(currentQuestion));
                gameObject.GetComponent<Text>().text = questions[currentQuestion];
            } 
        }

	}
}
